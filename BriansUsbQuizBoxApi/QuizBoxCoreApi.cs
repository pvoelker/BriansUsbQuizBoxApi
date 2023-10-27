using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Protocols;
using HidSharp;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Core communication interface for quiz boxes
    /// </summary>
    public class QuizBoxCoreApi : IDisposable
    {
        private bool _disposedValue;

        private HidStream? _stream = null;

        /// <summary>
        /// True if connected to a quiz box, otherwise false
        /// </summary>
        public bool IsConnected
        {
            get { return _stream != null; }
        }

        /// <summary>
        /// Attempt to connect to a quiz box
        /// </summary>
        /// <returns>True if connection successful, otherwise false</returns>
        /// <exception cref="MultipleDevicesException">More than one quiz box is detected</exception>
        /// <exception cref="InvalidOperationException">Already connected to a quiz box</exception>
        public bool Connect()
        {
            if (_stream == null)
            {
                var list = DeviceList.Local;
                //list.Changed += (sender, e) => Console.WriteLine("Device list changed.");

                var devices = list.GetHidDevices();

                HidDevice? box = null;
                try
                {
                    box = devices.SingleOrDefault(x => x.VendorID == 1240 && x.ProductID == 128 && x.GetManufacturer() == "Brians Box");
                }
                catch(InvalidOperationException ex)
                {
                    throw new MultipleDevicesException("More than one quiz box is connected", ex);
                }

                if (box != null)
                {
                    if (box.TryOpen(out _stream))
                    {
                        _stream.ReadTimeout = Timeout.Infinite;
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException("Already connected to a quiz box");
            }
        }

        /// <summary>
        /// Write a command to the quiz box
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="ArgumentNullException">Null was passed in for box command</exception>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        public void WriteCommand(BoxCommand command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            };

            if(_stream != null)
            {
                var data = command.BuildByteArray();
                _stream.Write(data);
            }
            else
            {
                throw new NotConnectedException("Must be connected to write a command");
            }
        }

        /// <summary>
        /// Synchronous quiz box status read
        /// </summary>
        /// <returns>Quiz box status, otherwise null</returns>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        public BoxStatus? ReadStatus()
        {
            if (_stream != null)
            {
                BoxStatus? retVal = null;

                var inputReportBuffer = new byte[BuzzerConstants.REPORT_LENGTH];

                int byteCount = _stream.Read(inputReportBuffer, 0, BuzzerConstants.REPORT_LENGTH);

                if (byteCount > 0)
                {
                    retVal = BoxStatus.Parse(inputReportBuffer);
                }

                return retVal;
            }
            else
            {
                throw new NotConnectedException("Must be connected to read status");
            }
        }

        /// <summary>
        /// Asynchronous quiz box status read
        /// </summary>
        /// <returns>Quiz box status, otherwise null</returns>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        public async Task<BoxStatus?> ReadStatusAsync()
        {
            if (_stream != null)
            {
                BoxStatus? retVal = null;

                var inputReportBuffer = new byte[BuzzerConstants.REPORT_LENGTH];

                int byteCount = await _stream.ReadAsync(inputReportBuffer, 0, BuzzerConstants.REPORT_LENGTH);

                if (byteCount > 0)
                {
                    retVal = BoxStatus.Parse(inputReportBuffer);
                }

                return retVal;
            }
            else
            {
                throw new NotConnectedException("Must be connected to read status");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_stream != null)
                    {
                        _stream.Dispose();
                        _stream = null;
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
