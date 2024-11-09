using MvvmSample.ViewModels;

namespace MvvmSample.Tests
{
  public class MvvmDemoViewModelTests
  {
    [Fact]
    public void SignCommand_ShouldNotBeExecutable_Initially()
    {
      var viewModel = new MvvmDemoViewModel();
      Assert.False(viewModel.SignCommand.CanExecute(null));
    }

    [Fact]
    public void SignCommand_ShouldBeExecutable_WhenAllFieldsAreValid()
    {
      var viewModel = new MvvmDemoViewModel
      {
        Name = "Wilhelm Brause",
        Place = "Köln",
        AcceptTerms = true
      };

      Assert.True(viewModel.SignCommand.CanExecute(null));
    }

    [Fact]
    public void SignCommand_ShouldNotBeExecutable_WhenNameIsMissing()
    {
      var viewModel = new MvvmDemoViewModel
      {
        Name = "",
        Place = "Köln",
        AcceptTerms = true
      };

      Assert.False(viewModel.SignCommand.CanExecute(null));
    }

    [Fact]
    public void SignCommand_ShouldNotBeExecutable_WhenPlaceIsMissing()
    {
      var viewModel = new MvvmDemoViewModel
      {
        Name = "Wilhelm Brause",
        Place = "",
        AcceptTerms = true
      };

      Assert.False(viewModel.SignCommand.CanExecute(null));
    }

    [Fact]
    public void SignCommand_ShouldNotBeExecutable_WhenTermsAreNotAccepted()
    {
      var viewModel = new MvvmDemoViewModel
      {
        Name = "Wilhelm Brause",
        Place = "Köln",
        AcceptTerms = false
      };

      Assert.False(viewModel.SignCommand.CanExecute(null));
    }
  }
}