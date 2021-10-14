using System.Collections.Generic;

namespace Inventory.Core.HelperObjects
{
    public class ServiceResponse
    {
        public List<string> errors { get; set; }

        public bool hasSuccess { get; set; }
    }
}
