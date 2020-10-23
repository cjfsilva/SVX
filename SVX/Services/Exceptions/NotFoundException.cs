using System;

namespace SVX.Services.Exceptions {
    public class NotFoundException : ApplicationException {
        public NotFoundException(string message) : base(message) {}
    }
}
