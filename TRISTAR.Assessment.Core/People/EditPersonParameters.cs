using TRISTAR.Assessment.Infrastructure;

namespace TRISTAR.Assessment.People
{
    /// <summary>
    /// Parameters to be sent to the server when modifying a <see cref="Person"/>.
    /// When updating a person, any property that is not set will be ignored.
    /// </summary>
    public class EditPersonParameters : PatchParametersBase
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
    }
}