using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musili.Tests.Mocks {
    class LoggerMock<T> : ILogger<T> {
        public IDisposable BeginScope<TState>(TState state) {
            return new ScopeMock();
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        }

        private class ScopeMock : IDisposable {
            public void Dispose() { }
        }
    }

    
}
