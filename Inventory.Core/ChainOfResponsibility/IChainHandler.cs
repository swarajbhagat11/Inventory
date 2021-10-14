namespace Inventory.Core.ChainOfResponsibility
{
    public interface IChainHandler
    {
        IChainHandler SetNext(IChainHandler handler);

        object Handle(object request = null);
    }
}
