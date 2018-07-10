using System;
using System.Threading;
using RemoteBotClient;

namespace Collabitama.Client.Helpers {
    public class AsyncBotInterface {
        private readonly IBotInterface _botInterface;
        private readonly AutoResetEvent _getInput;
        private readonly AutoResetEvent _gotInput;
        private readonly int _timeOutMillisecs;
        private string _input;

        public AsyncBotInterface(string apiKey, int timeOutMillisecs) {
            _botInterface = RemoteBotClientInitializer.Init(apiKey, false);
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
            _timeOutMillisecs = timeOutMillisecs;

            var inputThread = new Thread(Reader) {
                IsBackground = true
            };

            inputThread.Start();
        }

        private void Reader() {
            while (true) {
                _getInput.WaitOne();
                _input = _botInterface.ReadLine();
                _gotInput.Set();
            }
        }

        public string ReadLine() {
            _getInput.Set();

            if (_gotInput.WaitOne(_timeOutMillisecs)) {
                return _input;
            }

            throw new TimeoutException("Server did not provide data within the timelimit.");
        }

        public void WriteLine(string input) {
            _botInterface.WriteLine(input);
        }
    }
}