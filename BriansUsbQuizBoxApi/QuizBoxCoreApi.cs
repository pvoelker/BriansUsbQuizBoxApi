using BriansUsbQuizBoxApi.Exceptions;
using BriansUsbQuizBoxApi.Helpers;
using BriansUsbQuizBoxApi.Protocols;
using HidSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BriansUsbQuizBoxApi
{
    /// <summary>
    /// Core implementation for Brian's Quiz Box Communication Protocol
    /// </summary>
    public class QuizBoxCoreApi : IQuizBoxCoreApi
    {
        private bool _disposedValue;

        private HidStream? _stream = null;

        private QuizBoxTypeEnum? _connectedType = null;

#if DEBUG
        private BoxStatusReport? _lastBoxStatusReport = null;
#endif

        /// <inheritdoc/>
        public bool IsConnected
        {
            get { return _stream != null; }
        }

        public QuizBoxTypeEnum? ConnectedQuizBoxType
        {
            get { return _connectedType; }
        }

        /// <inheritdoc/>
        /// <exception cref="MultipleDevicesException">More than one quiz box is detected</exception>
        /// <exception cref="InvalidOperationException">Already connected to a quiz box</exception>
        public bool Connect()
        {
            if (_stream == null)
            {
                HidDevice? box = GetBriansBoxDevice();
                if(box != null)
                {
                    _connectedType = QuizBoxTypeEnum.BriansQuizBox;
                }
                else
                {
                    box = GetBasicQuizboxPlusDevice();
                    if(box != null)
                    {
                        _connectedType = QuizBoxTypeEnum.KirkmanQuizBox;
                    }
                }

                if (box != null)
                {
                    if (box.TryOpen(out _stream))
                    {
                        // Don't wait on reads
                        _stream.ReadTimeout = 0;
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

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Null was passed in for box command</exception>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        /// <exception cref="DisconnectionException">Quiz box has been disconnected</exception>
        public void WriteCommand(BoxCommandReport command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            };

            if(_stream != null)
            {
                var data = command.BuildByteArray();

                try
                {
                    _stream.Write(data);
                }
                catch(IOException ex)
                {
                    Disconnect();

                    throw new DisconnectionException("Quiz box has been disconnected", ex);
                }
            }
            else
            {
                throw new NotConnectedException("Must be connected to write a command");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        /// <exception cref="DisconnectionException">Quiz box has been disconnected</exception>
        public BoxStatusReport? ReadStatus()
        {
            if (_stream != null)
            {
                BoxStatusReport? retVal = null;

                var inputReportBuffer = new byte[BuzzerConstants.REPORT_LENGTH];

                int byteCount = 0;
                try
                {
                    byteCount = _stream.Read(inputReportBuffer, 0, BuzzerConstants.REPORT_LENGTH);
                }
                catch(IOException ex)
                {
                    Disconnect();

                    throw new DisconnectionException("Quiz box has been disconnected", ex);
                }

                if (byteCount > 0)
                {
                    retVal = BoxStatusReport.Parse(inputReportBuffer);
                }

#if DEBUG
                if (_lastBoxStatusReport != retVal)
                {
                    DebugHelpers.WriteLine(retVal == null ? "[null]" : retVal.ToString());
                }
                _lastBoxStatusReport = retVal;
#endif

                return retVal;
            }
            else
            {
                throw new NotConnectedException("Must be connected to read status");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="NotConnectedException">Not connected to a quiz box</exception>
        /// <exception cref="DisconnectionException">Quiz box has been disconnected</exception>
        public async Task<BoxStatusReport?> ReadStatusAsync()
        {
            if (_stream != null)
            {
                BoxStatusReport? retVal = null;

                var inputReportBuffer = new byte[BuzzerConstants.REPORT_LENGTH];

                int byteCount;
                try
                {
                    byteCount = await _stream.ReadAsync(inputReportBuffer, 0, BuzzerConstants.REPORT_LENGTH);
                }
                catch(IOException ex)
                {
                    Disconnect();

                    throw new DisconnectionException("Quiz box has been disconnected", ex);
                }

                if (byteCount > 0)
                {
                    retVal = BoxStatusReport.Parse(inputReportBuffer);
                }

                return retVal;
            }
            else
            {
                throw new NotConnectedException("Must be connected to read status");
            }
        }

        /// <inheritdoc/>
        public void Disconnect()
        {
            _connectedType = null;
            _stream?.Dispose();
            _stream = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Disconnect();
                }

                _disposedValue = true;
            }
        }

        private HidDevice? GetBriansBoxDevice()
        {
            // NOTE: The PID for Brian's Quiz Boxes is NOT unique, so the manufacturer name must be looked at
            var devices = DeviceList.Local.GetHidDevices(0x04D8, 0x0080);

            HidDevice? box = null;
            try
            {
                box = devices.SingleOrDefault(x => x.GetManufacturer() == "Brians Box");
            }
            catch (InvalidOperationException ex)
            {
                throw new MultipleDevicesException("More than one Brian's Quiz Box is connected", ex);
            }

            return box;
        }

        private HidDevice? GetBasicQuizboxPlusDevice()
        {
            // NOTE: The PID for the Kirkman Basic Quizbox Plus IS unique
            var devices = DeviceList.Local.GetHidDevices(0x04D8, 0xE5DC);

            HidDevice? box;
            try
            {
                box = devices.SingleOrDefault();
            }
            catch (InvalidOperationException ex)
            {
                throw new MultipleDevicesException("More than one Kirkman Basic Quizbox Plus is connected", ex);
            }

            return box;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
