using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nott.Models;

namespace Nott.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
	private AppSettings settings;

	[ObservableProperty]
	bool shuffle;

	[RelayCommand]
	public void Saving()
	{
		settings.setShuffle(shuffle);
		settings.Save();
	}

	public SettingsViewModel(AppSettings s)
	{
		this.settings = s;
		shuffle = settings.getShuffle();
	}
}