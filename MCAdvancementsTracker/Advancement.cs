using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCAdvancementsTracker
{
    class Advancement
    {
        public string Name { get; }
        public string Description { get; }
        public string Parent { get; }
        public string Requirement { get; }
        public string Category { get; }
        public string Id { get; }

        public Advancement(string name, string description, string parent, string requirement, string id, string category)
        {
            this.Name = name;
            this.Description = description;
            this.Parent = parent;
            this.Requirement = requirement;
            this.Id = id;
            this.Category = category;

        }
    }
}
