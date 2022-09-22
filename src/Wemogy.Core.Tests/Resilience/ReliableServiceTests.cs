using System;
using System.Threading.Tasks;
using Wemogy.Core.Resilience;
using Xunit;

namespace Wemogy.Core.Tests.Resilience
{
    public class ReliableServiceTests
    {
        private int _callCount;
        private Task SuccessfulAfterTreeTimes()
        {
            _callCount++;

            Console.WriteLine($"Call count: {_callCount}");

            if (_callCount < 3)
            {
                throw new Exception();
            }

            return Task.CompletedTask;
        }

        private async Task AlwaysSuccessful()
        {
            await Task.Delay(2000);
        }

        private async Task AlwaysFailed()
        {
            await Task.Delay(100);

            throw new Exception();
        }

        private async Task AlwaysIndexOutOfRangeException()
        {
            await Task.Delay(100);
            throw new IndexOutOfRangeException();
        }

        [Fact]
        public async Task RunExponential_ShouldWork_1()
        {
            await ReliableService.RunExponential<Exception>(() => AlwaysSuccessful());
        }

        [Fact]
        public async Task RunExponential_ShouldWork_AlwaysFailed()
        {
            await Assert.ThrowsAsync<Exception>(() =>
                ReliableService.RunExponential<Exception>(() => AlwaysFailed()));
        }

        [Fact]
        public async Task RunExponential_ShouldWork()
        {
            await ReliableService.RunExponential<Exception>(() => SuccessfulAfterTreeTimes());
        }

        [Fact]
        public async Task RunExponential_ShouldThrowOtherExceptions()
        {
            await Assert.ThrowsAsync<IndexOutOfRangeException>(() =>
                ReliableService.RunExponential<UnauthorizedAccessException>(AlwaysIndexOutOfRangeException));
        }
    }
}
