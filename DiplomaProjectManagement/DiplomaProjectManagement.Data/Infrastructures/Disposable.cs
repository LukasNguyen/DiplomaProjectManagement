using System;

namespace DiplomaProjectManagement.Data.Infrastructures
{
    //Cơ chế : tự tắt được đối tượng khi không dùng đến
    public class Disposable : IDisposable
    {
        private bool _isDisposed;

        ~Disposable()
        {
            Dispose(false); //Khi hủy đối tượng dispose thì không dispose
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // khi dispose thì thu hoạch bộ nhớ
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }
            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }
}