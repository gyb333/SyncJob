using Volo.Abp.Settings;

namespace SyncJob
{
    public class SettingDefinitionProvider : Volo.Abp.Settings.SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from SyncJobSettings class.
             * context.Add(new SettingDefinition(BookStoreSettings.MySetting1));
             */
            context.Add(new SettingDefinition(Settings.MySettingName) );
        }
    }
}