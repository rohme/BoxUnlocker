
namespace BoxUnlocker.Models
{
    public class Operation
    {
        public Operation()
            : this(OperationType.None, 0)
        {
        }
        public Operation(OperationType iOperationType, int iInputNumber)
        {
            this.OperationType = iOperationType;
            this.InputNumber = iInputNumber;
        }
        public OperationType OperationType { get; set; }
        public int InputNumber { get; set; }
    }
}
