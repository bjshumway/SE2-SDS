namespace ActorNS {

    // Resource is designed to be a nice way to manage health/mana/stamina
    // It has methods for safely adding/subtracting as well as a max value
    public class Resource {
        private double _value;
        private double _maxValue;
        
        public double value { // current value the resource is at
            get {
                return _value;
            }
        }

        public double maxValue { // max value the resource can be
            get {
                return _maxValue;
            }
        }

        /// <summary>
        /// Constructor for Resource
        /// </summary>
        /// <param name="maxValue">The maximum value for the Resource</param>
        /// <param name="value">The current value for the resource (leave at -1 for full)</param>
        public Resource(double maxValue, double value = -1) {
            _maxValue = maxValue;
            _value = (value == -1) ? maxValue : value;
        }

        /// <summary>
        /// Adds the specified number to Resource.value
        /// </summary>
        /// <param name="valueToAdd">Number to add to Resource.value</param>
        public void add(double valueToAdd) {
            double newValue = _value + valueToAdd;
            setValue(newValue);
        }

        /// <summary>
        /// Subtracts the specified number from Resource.value
        /// </summary>
        /// <param name="valueToSubtract">Number to subtract from Resource.value</param>
        public void subtract(double valueToSubtract) {
            add(-valueToSubtract);
        }

        /// <summary>
        /// Sets Resource.value to the specified double (not going over maxValue or under 0)
        /// </summary>
        /// <param name="newValue">New value for Resource.value</param>
        public void setValue(double newValue) {
            if (newValue > maxValue) { // too high
                _value = maxValue; // set to max
            } else if (newValue < 0) { // below 0
                _value = 0; // set to 0
            } else { // within range
                _value = newValue; // just add it
            }
        }

        /// <summary>
        /// Sets Resource.maxValue to the specified double
        /// </summary>
        /// <param name="newMaxValue">New max value</param>
        /// <param name="resetValue">True if we want the Resource to be 'full'</param>
        public void setMaxValue(double newMaxValue, bool resetValue) {
            _maxValue = newMaxValue;

            // if we want to reset, or if the current value is greater than the new max
            if (resetValue || value > maxValue) {
                _value = maxValue; // set the current value to the new max
            }
        }
    }
}
