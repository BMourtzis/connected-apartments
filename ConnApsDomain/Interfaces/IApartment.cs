using System.Collections.Generic;

namespace ConnApsDomain
{
    /// <summary>
    /// Interface of the Apartment Structure
    /// </summary>
    public interface IApartment
    {
        /// <summary>
        /// The Id of the Apartment
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The Level that the apartment is on
        /// </summary>
        string Level { get; }

        /// <summary>
        /// The Number of the Apartment
        /// </summary>
        string Number { get; }

        /// <summary>
        /// The Building Id the apartment is in
        /// </summary>
        int BuildingId { get; }

        /// <summary>
        /// The Building that the apartment is in
        /// </summary>
        IBuilding Building { get; }

        /// <summary>
        /// The Number of tenants allowed to live in the apartment
        /// </summary>
        int TenantsAllowed { get; }

        /// <summary>
        /// The primary direction that the apartment is facing
        /// </summary>
        string FacingDirection { get; }

        /// <summary>
        /// A List of the Tenants living in the Apartment
        /// </summary>
        IEnumerable<ITenant> Tenants { get; }
    }
}