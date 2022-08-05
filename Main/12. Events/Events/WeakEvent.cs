using System;
using System.Windows;

namespace MailManagerWeakEvent
{
    public class NewMailEventArgs : EventArgs
    {
        // 1. Type defining information passed to receivers of the event
        public NewMailEventArgs(String from, String to, String subject, String body)
        {
            this.from = from;
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
        public readonly String from, to, subject, body;
    }
    class MailPublisher
    {

        // 2. The event itself
        public event EventHandler<NewMailEventArgs> NewMail;

        // 3. Protected, virtual method responsible for notifying registered objects of the event
        protected virtual void OnNewMail(NewMailEventArgs e)
        {
            // Has any objects registered interest with our event? 
            if (NewMail != null)
            {
                NewMail(this, e);     // Yes, notify all the objects
            }
        }

        // 4. Method that translates the input into the desired event
        //    This method is called when a new e-mail message arrives
        public void SimulateArrivingMsg(String from, String to, String subject, String body)
        {
            //Construct an object to hold the information to pass to the receivers of our notification
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject, body);

            // Call our virtual method notifying our object that the event occurred. If no type overrides
            // this method, our object will notify all the objects that registered interest in the event
            OnNewMail(e);
        }
    }
    class Fax
    {
        // This is the method that the MailManager will call when a new e-mail message arrives
        public void FaxMsg(object sender, NewMailEventArgs e)
        {
            // 'sender' identifies the MailManager in case we want to communicate back to it.
            // 'e' identifies the additional event information that the MailManager wants to give us.
            // Normally, the code here would fax the e-mail message.
            // This test implementation displays the info on the console
            Console.WriteLine("Faxing mail message:");
            Console.WriteLine("   From: {0}\n   To: {1}\n   Subject: {2}\n   Body: {3}\n",
               e.from, e.to, e.subject, e.body);
        }
    }

    class CellPhone
    {
         // This is the method that the MailManager will call
        // when a new e-mail message arrives
        public void PhoneMsg(
           Object sender, NewMailEventArgs e)
        {
            // 'sender' identifies the MailManager in case we want to communicate back to it.
            // 'e' identifies the additional event information that the MailManager wants to give us.
            // Normally, the code here would send the e-mail message to a phone.
            // This test implementation displays the info on the console
            Console.WriteLine("Send mail message to cellPhone:");
            Console.WriteLine("   From: {0}\n   To: {1}\n   Subject: {2}\n   Body: {3}\n",
                               e.from, e.to, e.subject, e.body);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Construct a MailPublisher object
            MailPublisher mm = new MailPublisher();

            // Construct a Fax object 
            Fax fax = new Fax();

            // Construct a CellPhone object 
            CellPhone phone = new CellPhone();

            //Register weak events by WeakEventManager
            WeakEventManager<MailPublisher, NewMailEventArgs>.AddHandler(mm, "NewMail", fax.FaxMsg);
            WeakEventManager<MailPublisher, NewMailEventArgs>.AddHandler(mm, "NewMail", phone.PhoneMsg);

            // Simulate an incoming mail message
            mm.SimulateArrivingMsg("Google", "Me",
               "Job Invitation",
               "We want you to work in our company");

            // Unregister weak event for the Fax object by WeakEventManager
            WeakEventManager<MailPublisher, NewMailEventArgs>.RemoveHandler(mm, "NewMail", fax.FaxMsg);

            // Simulate an incoming mail message
            mm.SimulateArrivingMsg("Me", "Google",
               "Re: Job Invitation",
               "Thanks, but I am too busy in Amdaris. May be later.");

            Console.ReadLine();
        }
    }
}
