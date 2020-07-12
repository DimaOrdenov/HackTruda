using System.ComponentModel;

namespace HackTruda.Definitions.Enums
{
    /// <summary>
    /// Перечисление поддерживаемых социальных сетей.
    /// </summary>
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
