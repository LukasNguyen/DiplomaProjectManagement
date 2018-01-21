using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaProjectManagement.Data.Infrastructures
{
    //Cơ chế : tự tắt được đối tượng khi không dùng đến
    public class Disposable : IDisposable
    {
        private bool isDisposed;

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
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }
            isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
    }
}
