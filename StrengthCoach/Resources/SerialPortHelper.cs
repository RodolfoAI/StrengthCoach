using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace StrengthCoach.Resources
{
    public static class SerialPortHelper
 {
   /// <summary>
        /// Gets all available COM ports on the system
 /// </summary>
        /// <returns>List of available COM port names</returns>
        public static List<string> GetAvailableComPorts()
        {
            try
         {
                return SerialPort.GetPortNames().OrderBy(p => p).ToList();
            }
   catch (Exception ex)
   {
         System.Windows.MessageBox.Show($"Error al escanear puertos COM: {ex.Message}", 
     "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
          return new List<string>();
        }
     }

        /// <summary>
    /// Checks if a specific COM port is available
    /// </summary>
        /// <param name="portName">The COM port name to check (e.g., "COM5")</param>
        /// <returns>True if the port is available, false otherwise</returns>
        public static bool IsPortAvailable(string portName)
        {
   return GetAvailableComPorts().Contains(portName);
        }
    }
}
