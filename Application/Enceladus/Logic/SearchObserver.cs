using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus
{
    class SearchObserver : StateObserver
    {
        public IList<IObserver> Observers { get; set; }
        public bool HasResults { get; set; }

        public SearchObserver()
        {
            this.Observers = new List<IObserver>();
            this.HasResults = false;
        }

        public override void Attach(IObserver observer)
        {
            if (!this.Observers.Contains(observer))
                this.Observers.Add(observer);

            observer.Update(this.HasResults);
        }

        public override void Deattach(IObserver observer)
        {
            if (this.Observers.Contains(observer))
                this.Observers.Remove(observer);
        }

        public override void Notify()
        {
            foreach (var observer in this.Observers)
            {
                observer.Update(this.HasResults);
            }
        }
    }
}
