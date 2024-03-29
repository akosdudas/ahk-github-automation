using System;

namespace Ahk.GradeManagement.Data.Entities
{
    [Newtonsoft.Json.JsonConverter(typeof(Helper.DisabledJsonConverter))]
    public class BranchCreateEvent : StatusEventBase
    {
        public const string TypeName = "BranchCreateEvent";

        public BranchCreateEvent(string id, string repository, DateTime timestamp, string branch)
            : base(id, repository, timestamp)
        {
            this.Branch = branch;
        }

        public override string Type => TypeName;
        public string Branch { get; }
    }
}
