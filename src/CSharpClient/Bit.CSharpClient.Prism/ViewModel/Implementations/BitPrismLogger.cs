﻿#define Debug

using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bit.ViewModel.Implementations
{
    public class BitPrismLogger : ILogger
    {
        public virtual void Log(string message, IDictionary<string, string> properties)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(message);
        }

        public virtual void Log(string message, Category category, Priority priority)
        {
            if (category == Category.Exception)
            {
                Report(new Exception(message), new Dictionary<string, string>
                {
                    { nameof(category), category.ToString() },
                    { nameof(priority), priority.ToString() }
                });
            }
            else if (Debugger.IsAttached)
            {
                Debug.WriteLine(message);
            }
        }

        public virtual void Report(Exception ex, IDictionary<string, string> properties)
        {
            BitExceptionHandler.Current.OnExceptionReceived(ex, properties);
        }

        public virtual void TrackEvent(string name, IDictionary<string, string> properties)
        {
            
        }
    }
}
