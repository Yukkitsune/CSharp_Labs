using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CSharp_Lab15
{
    public interface IFileSystemObserver
    {
        void OnFileCreated(string path);
        void OnFileDeleted(string path);
    }
    public class SimpleFileSystemWatcher
    {
        private readonly string _directoryPath;
        private readonly List<IFileSystemObserver> _observers = new List<IFileSystemObserver>();
        private readonly System.Timers.Timer _timer;
        private HashSet<string> _existingFiles;
        public SimpleFileSystemWatcher(string directoryPath, double interval)
        {
            _directoryPath = directoryPath;
            _timer = new System.Timers.Timer(interval);
            _timer.Elapsed += CheckForeChanges;
            _existingFiles = new HashSet<string>(Directory.GetFiles(directoryPath));
            _timer.Start();
        }
        public void Subscribe(IFileSystemObserver observer)
        {
            _observers.Add(observer);
        }
        public void Unsubscribe(IFileSystemObserver observer)
        {
            _observers.Remove(observer);
        }
        private void CheckForeChanges(object sender, ElapsedEventArgs e) {
            var currentFiles = new HashSet<string>(Directory.GetFiles(_directoryPath));
            foreach (var file in currentFiles.Except(_existingFiles))
            {
                NotifyFileCreated(file);
            }
            foreach (var file in _existingFiles.Except(currentFiles))
            {
                NotifyFileDeleted(file);
            }
            _existingFiles = currentFiles;
        }
        private void NotifyFileCreated(string filePath) {
            foreach (var observer in _observers)
            {
                observer.OnFileCreated(filePath);
            }
        }
        private void NotifyFileDeleted(string filePath) {
            foreach (var observer in _observers)
            {
                observer.OnFileDeleted(filePath);
            }
        }
    }
}
