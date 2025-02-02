using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using LazyFramework.DX.Shared.Models;
using LazyFramework.DX.Shared.Models.StateMachine;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
using UiPath.CodedWorkflows.Models;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.Api.Models;
using UiPath.Testing.Activities.Models;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
#nullable enable
namespace LazyFramework.DX.Shared.Models.StateMachine
{

    public abstract class WorkflowSlots<TStateData> : DictionaryObject
    {
        public Dictionary<string, Workflow<TStateData>> Data { get; set; } = new();
        public void RegisterSlot(string name, Workflow<TStateData> slot)
        {
            this[name] = slot;
        }
        public WorkflowSlots() { }
    }

    public class Workflow<TStateData> : CodedWorkflow
    {
        public new ICodedWorkflowServices services {get; set;}
        public Workflow() { }
        public Workflow(ICodedWorkflowServices _services)
        {
            services = _services;
        }
        public virtual TStateData Execute(TStateData state)
        {
            return state;
        }
    }




    public abstract class State<TStateData, TSlots> : CodedWorkflow where TSlots : WorkflowSlots<TStateData>, new()
    {
        public abstract string Name { get; set; }
        public TSlots Slots { get; set; }
        public abstract void Execute(TStateData state);

        public void RegisterSlot(string name, Workflow<TStateData>? slot)
        {
            Log($"Registering slot {name}");
            Slots.Data.Add(name, slot);
        }
    }
    public abstract class StateSlots<TStateData> : DictionaryObject
    {
        public Dictionary<string, Workflow<TStateData>> Slots { get; private set; } = new();

        public void RegisterSlot(string name, Workflow<TStateData> slot)
        {
            if (Slots.ContainsKey(name))
            {
                throw new ArgumentException($"Slot '{name}' is already registered.");
            }

            Slots[name] = slot;
        }
    }


    public class StateMachine<TStateData> : CodedWorkflow
    {
        private StateSlots<TStateData> States { get; set; }
        public readonly Stack<Workflow<TStateData>> _stateStack = new();
        public readonly FixedSizeQueue<string> StackHistory = new(100);
        public TStateData Data { get; set; }

        public void TransitionTo(Workflow<TStateData> state)
        {
            _stateStack.Push(state);
        }

        public void RegisterStates(StateSlots<TStateData> states)
        {
            States = states;
        }

        public void Initialize(Workflow<TStateData> initialState, Workflow<TStateData> endState)
        {
            TransitionTo(initialState);
            TransitionTo(endState);
        }

        // Entry
        public virtual void Execute(params object[] param)
        {
            while (_stateStack.Count > 0)
            {
                var currentState = _stateStack.Pop();
                try
                {
                    currentState.Execute(Data);
                }
                catch (Exception ex)
                {
                    Log($"Error in state execution: {ex.Message}");
                    continue;
                }
            }
        }
    }
}