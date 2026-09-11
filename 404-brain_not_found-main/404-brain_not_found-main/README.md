# 404-brain_not_found
INF 164 group project

Person 1 contributions 

Created classes: Song, PLaylistModel, DataManager, User.

Form1:
 
// Load persistent user data using Serialization on start
_usersList = DataManager.LoadUsers();

// This method fixes the CS1061 error
public void RefreshUserData()
{
    _usersList = DataManager.LoadUsers();
}
// Checks the loaded List<User> from persistent json file
User matchingUser = _usersList.FirstOrDefault(u =>
    u.Username.Equals(inputUser, StringComparison.OrdinalIgnoreCase) &&
    u.Password.Equals(inputPass, StringComparison.Ordinal));

_homePage.setCurrentUserloggedin(finalUser);
_homePage.Show();
this.Hide();

Authenticates users against serialized JSON data, reloads updated user lists, and launches the main app.

Registrationpage:

// 1. Load existing users from the serialized file into a C# List<User>
List<User> currentUsers = DataManager.LoadUsers();

// 2. Verify that the username does not already exist in the list
if (currentUsers.Any(u => u.Username.Equals(newUserName, StringComparison.OrdinalIgnoreCase)))
{
    ShowValidationError("Username already exists. Please choose another one.");
    return;
}

// 3. Create a new User object and add it to our List<User>
currentUsers.Add(new User(newUserName, newUserPass));

// 4. Save the updated list using C# Serialization
if (DataManager.SaveUsers(currentUsers))
{
    MessageBox.Show("Registration successful! You can now log in.",
        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

string userPattern = @"^[a-zA-Z0-9]{3,15}$";
string passPattern = @"^(?=.*[*&^%@!#]).{3,15}$";

if (!Regex.IsMatch(regUser, userPattern))
{
    ShowValidationError("Invalid Username. Must be 3 - 15 alphanumeric characters.");
    return false;
}
_loginPage.RefreshUserData();
_loginPage.Show();
this.Close();

Validates new credentials, appends user objects, serializes updated account data, and refreshes the login state

Homepage:
private void LoadSavedPlaylists()
{
    try
    {
        if (File.Exists(savedPlaylistsFile))
        {
            string json = File.ReadAllText(savedPlaylistsFile);
            var customPlaylists = JsonSerializer.Deserialize<List<PlaylistModel>>(json);

            if (customPlaylists != null)
            {
                foreach (var playlist in customPlaylists)
                {
                    if (!string.IsNullOrEmpty(playlist.CoverImagePath) && File.Exists(playlist.CoverImagePath))
                    {
                        playlist.CoverImage = Image.FromFile(playlist.CoverImagePath);
                    }

                    if (!userPlaylists.Any(p => p.Title.Equals(playlist.Title, StringComparison.OrdinalIgnoreCase)))
                    {
                        userPlaylists.Add(playlist);
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Could not load saved playlists: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

public void SaveCustomPlaylists()
{
    try
    {
        string[] defaults = { "Favourites", "Vibes", "Sad Songs", "Study" };
        var customPlaylists = userPlaylists
            .Where(p => !defaults.Contains(p.Title, StringComparer.OrdinalIgnoreCase))
            .ToList();

        string json = JsonSerializer.Serialize(customPlaylists, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(savedPlaylistsFile, json);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Could not save playlists: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

// Triggered when a new playlist is added from Form2
public void AddPlaylistFromForm2(PlaylistModel newPlaylist)
{
    // ... validation checks ...
    userPlaylists.Add(newPlaylist);
    SaveCustomPlaylists(); // TRIGGERS JSON SAVE IMMEDIATELY
}

// Triggered when an audio file is uploaded and assigned to playlists
private void btnUploadSong_Click(object sender, EventArgs e)
{
    // ... file selection and assignment ...
    SaveCustomPlaylists(); // SAVE SONG ADDITIONS TO JSON
}

Serializes non-default playlists to JSON, restores playlists from disk, and auto-saves on changes.

Form2:

if (openFileDialog.ShowDialog() == DialogResult.OK)
{
    selectedImagePath = openFileDialog.FileName;

    using (FileStream stream = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
    {
        picCreate.Image = Image.FromStream(stream);
    }
}

// Create a full PlaylistModel instance with title, image, and file path
PlaylistModel newPlaylist = new PlaylistModel(playlistTitle, picCreate.Image, selectedImagePath);

HomePage mainHome = (HomePage)Application.OpenForms["HomePage"];

if (mainHome != null)
{
    // Pass the model to HomePage so it gets saved to list AND serialized to JSON
    mainHome.AddPlaylistFromForm2(newPlaylist);
    this.Close();
}

Captures playlist cover file paths and passes constructed models to HomePage for JSON serialization.
