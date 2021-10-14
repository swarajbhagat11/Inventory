namespace Inventory.Core.ChainOfResponsibility
{
    public abstract class ChainHandler : IChainHandler
    {
        private IChainHandler _handler;

        public IChainHandler SetNext(IChainHandler handler)
        {
            this._handler = handler;
            return handler;
        }

        public virtual object Handle(object request = null)
        {
            if (this._handler != null)
            {
                return this._handler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}
