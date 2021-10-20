using System;
namespace medicijn.Models.Dosering
{
    public class DoseringOption : BaseViewModel
    {
        public int Id { get; set; }
        public string Option { get; set; }
    }

    public class DoseringTakeInOption : DoseringOption
    {
        public string Amount { get; set; }
    }

    public class UsageOption : DoseringOption
    { 
    }

    public class RepeatedOption : DoseringOption
    { 
    }
}
