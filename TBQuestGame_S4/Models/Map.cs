using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestGame_S1.Models
{
    public class Map
    {
        #region  Fields
        private List<Location> _locations;
        private Location _currentLocation;
        private ObservableCollection<Location> _accessibleLocations;
        #endregion

        #region Properties
        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessibleLocations; }
            set { _accessibleLocations = value; }
        }
        #endregion

        #region Constructors

        public Map()
        {
            _locations = new List<Location>();
        }

        #endregion

        #region Methods

        public void Move(Location location)
        {
            _currentLocation = location;
        }

        #endregion
    }
}
