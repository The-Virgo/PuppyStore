using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PuppyStoreFinal.Models
{
    public class Event
    {
        /// <summary>
        /// auto-assigned unique ID for each event
        /// </summary>
        [Key]
        public int EventId { get; set; }

        /// <summary>
        /// The title of an event as is seen by the user
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date an event takes place
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// The description of an event
        /// </summary>
        public string Description { get; set; }
    }
}
