using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetLicensingClient.Entities
{
    /// <summary>
    /// Defines interface common for all NetLicensing Entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Returns printable representation of an entity.
        /// </summary>
        String ToString();
    }
}
