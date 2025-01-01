using System;
using System.Collections.Generic;
using System.Data;
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

namespace LazyFramework.DX.Shared.Models
{
    public class FixedSizeQueue<T> : Queue<T>
    {
        private readonly int _capacity;

        public FixedSizeQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            }
            _capacity = capacity;
        }

        public new void Enqueue(T item)
        {
            // If the queue is full, remove the oldest element
            if (Count == _capacity)
            {
                Dequeue(); // Remove the first element
            }

            // Add the new item
            base.Enqueue(item);
        }
    }
}