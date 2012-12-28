namespace CmdPrompt
{
    public static class DelegateCommandDescriptionProviderExtensions
    {
        public static CommandSyntax.IState2 AddCommand(this DelegateCommandDescriptionProvider provider, string name, string description = null)
        {
            CommandSyntax.IState0 implementer = new SyntaxImplementer(provider);
            return implementer.AddCommand(name, description);
        }
    }
}
