using System;

namespace MailManagerEvent
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
    class MailManager
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
        // Pass the MailManager object to the constructor
        public Fax(MailManager mm)
        {
            // Register our callback with MailManager's NewMail event
            mm.NewMail += FaxMsg;
        }

        // This is the method that the MailManager will call when a new e-mail message arrives
        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            // 'sender' identifies the MailManager in case we want to communicate back to it.
            // 'e' identifies the additional event information that the MailManager wants to give us.
            // Normally, the code here would fax the e-mail message.
            // This test implementation displays the info on the console
            Console.WriteLine("Faxing mail message:");
            Console.WriteLine("   From: {0}\n   To: {1}\n   Subject: {2}\n   Body: {3}\n",
               e.from, e.to, e.subject, e.body);
        }

        public void Unregister(MailManager mm)
        {
            // Unregister ourself with MailManager's NewMail event
            mm.NewMail -= FaxMsg;
        }
    }

    class CellPhone
    {
        // Pass the MailManager object to the constructor
        public CellPhone(MailManager mm)
        {
            // Register our callback with MailManager's NewMail event
            mm.NewMail += PhoneMsg;
        }

        // This is the method that the MailManager will call
        // when a new e-mail message arrives
        private void PhoneMsg(
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
            // Construct a MailManager object
            MailManager mm = new MailManager();

            // Construct a Fax object passing it the MailManager object
            Fax fax = new Fax(mm);

            // Construct a CellPhone object passing it the MailManager object
            CellPhone pager = new CellPhone(mm);

            // Simulate an incoming mail message
            mm.SimulateArrivingMsg("Google", "Me",
               "Job Invitation",
               "We want you to work in our company");

            // Force the Fax object to unregister itself with the MailManager
            fax.Unregister(mm);

            // Simulate an incoming mail message
            mm.SimulateArrivingMsg("Me", "Google",
               "Re: Job Invitation",
               "Thanks, but I am too busy in Amdaris. May be later.");

            Console.ReadLine();
        }
    }
}
