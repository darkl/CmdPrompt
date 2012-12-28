namespace CmdPrompt
{
    public interface ICommandDescriptionProvider
    {
        ICommandDescription[] GetCommands();
    }
}