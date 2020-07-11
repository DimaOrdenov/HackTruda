using System;
using System.ComponentModel;

namespace HackTruda.Definitions.Enums
{
    public enum SocialAuthType
    {
        [Description("Vkontakte")]
        Vk,

        [Description("Ok")]
        Ok,

        [Description("Facebook")]
        Fb,

        [Description("Google")]
        Google,
    }
}
