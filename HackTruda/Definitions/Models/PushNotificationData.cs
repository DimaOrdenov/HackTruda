using System;
using System.ComponentModel.DataAnnotations;
using HackTruda.Definitions.Enums;
using Newtonsoft.Json;

namespace HackTruda.Definitions.Models
{
    public class PushNotificationData
    {
        [JsonProperty(PropertyName = "tapAction")]
        private string _tapAction;

        [JsonProperty(PropertyName = "title", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "sound", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Sound { get; set; } = "default";

        [JsonProperty(PropertyName = "isShow", Required = Required.Always)]
        [Required]
        public bool IsShow { get; set; } = true;

        [JsonIgnore]
        public PushNotificationTapActionType TapAction
        {
            get
            {
                Enum.TryParse(_tapAction, out PushNotificationTapActionType action);

                return action;
            }
            set => _tapAction = value.ToString();
        }
    }
}
