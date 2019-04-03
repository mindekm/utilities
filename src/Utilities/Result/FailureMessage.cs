namespace Utilities
{
    using System;

    [Serializable]
    public class FailureMessage : FailureDetails<string>
    {
        public static readonly FailureMessage Unspecified = new FailureMessage("Unspecified error has occured.");

        public FailureMessage(string details, FailureLevel level = FailureLevel.Error)
            : base(details, level)
        {
        }
    }
}