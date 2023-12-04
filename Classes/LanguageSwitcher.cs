using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaHarbor.Classes
{
    public class LanguageManager
    {
        public string updateNotification { get; private set; }
        public string newUpdateText { get; private set; }
        public string noUpdateMessage { get; private set; }
        public string noUpdateText { get; private set; }
        public string metroTabPage2Text { get; private set; }
        public string metroTabPage5Text { get; private set; }
        public string metroTabPage4Text { get; private set; }
        public string button2Text { get; private set; }
        public string metroButton2Text { get; private set; }
        public string songkey { get; private set; }
        public string song { get; private set; }
        public string songURL { get; private set; }
        public string pleasecheckapp { get; private set; }
        public string entersongpodcasturl { get; private set; }
        public string musicDownloadErrorText { get; private set; }
        public string selectdownloadlocfirst { get; private set; }
        public string enteratleastone { get; private set; }
        public string filenamingText { get; private set; }
        public string enterTextMessage { get; private set; }
        public string downloadError { get; private set; }
        public string openText { get; private set; }
        public string folderNotFoundText { get; private set; }
        public string downloadLocationText { get; private set; }
        public string howManyText { get; private set; }
        public string movieText { get; private set; }
        public string playlistText { get; private set; }
        public string processCompleteText { get; private set; }
        public string saveLocationText { get; private set; }
        public string seriesText { get; private set; }
        public string shutdownPCText { get; private set; }
        public string startText { get; private set; }
        public string error { get; private set; }
        public string selectFolderText { get; private set; }
        public string downloads { get; private set; }
        public string noUpdate { get; private set; }
        public string alreadyUpdatedText { get; private set; }
        public string updatingPleaseWait { get; private set; }
        public string ytdlpUpdate { get; private set; }
        public string updateError { get; private set; }
        public string updateCompletedMessage { get; private set; }
        public string updateCompleted { get; private set; }
        public string selectDownloadLocationText { get; private set; }
        public string inputCorrectint { get; private set; }
        public string movieseriesaudioformat { get; private set; }
        public string movieseriesvideoformat { get; private set; }
        public string movieserieskeyformat { get; private set; }
        public string moviename { get; private set; }
        public string movieyear { get; private set; }
        public string seriesname { get; private set; }
        public string seriesseason { get; private set; }
        public string seriesstartep { get; private set; }
        public string moviecompleted { get; private set; }
        public string episodenumber { get; private set; }
        public string downloadcompleteText { get; private set; }
        public string downloadCompleteMessage { get; private set; }
        public string shuttingdownText { get; private set; }
        public string enteryoutubelinkText { get; private set; }
        public string doformatselectionText { get; private set; }
        public string button1Text { get; private set; }
        public string metroCheckBox1Text { get; private set; }
        public string autoUpdateText { get; private set; }
        public string metroLabel5Text { get; private set; }
        public string richTextBox1Text { get; private set; }
        public string metroLabel2Text { get; private set; }
        public string metroLabel3Text { get; private set; }
        public string metroButton3Text { get; private set; }
        public string metroButton4Text { get; private set; }
        public string metroLabel4Text { get; private set; }
        public string metroButton1Text { get; private set; }
        public string metroCheckBox2Text { get; private set; }
        public string downloadText { get; private set; }
        public string selectQuality {  get; private set; }
        public string appTheme { get; private set; }
        public string darkTheme { get; private set; }
        public string lightTheme { get; private set; }
        public string copyURL { get; private set; }
        public string searchText { get; private set; }
        public string youtubeSearch {  get; private set; }


        public void ChangeLanguage(string cultureCode, string newUpdateText)
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;

            if (currentCulture == "tr-TR")
            {
                SetCommonTranslations();
            }
            else if (currentCulture == "en-US")
            {
                SetCommonTranslations();
            }
            else
            {
                SetCommonTranslations();
            }
            
        }
        public void SetCommonTranslations()
        {
            copyURL = Properties.Resources.copyURL;
            searchText = Properties.Resources.searchText;
            youtubeSearch = Properties.Resources.youtubeSearch;
            lightTheme = Properties.Resources.lightTheme;
            darkTheme = Properties.Resources.darkTheme;
            appTheme = Properties.Resources.appTheme;
            selectQuality = Properties.Resources.selectQuality;
            songkey = Properties.Resources.songkey;
            song = Properties.Resources.songurl;
            songURL = Properties.Resources.songurl;
            pleasecheckapp = Properties.Resources.pleasecheckapp;
            entersongpodcasturl = Properties.Resources.entersongpodcasturl;
            musicDownloadErrorText = Properties.Resources.musicDownloadErrorText;
            selectdownloadlocfirst = Properties.Resources.selectdownloadlocfirst;
            enteratleastone = Properties.Resources.enteratleastone;
            filenamingText = Properties.Resources.filenamingText;
            enterTextMessage = Properties.Resources.enterTextMessage;
            downloadError = Properties.Resources.downloadError;
            openText = Properties.Resources.openText;
            folderNotFoundText = Properties.Resources.folderNotFoundText;
            downloadLocationText = Properties.Resources.downloadLocationText;
            downloadText = Properties.Resources.downloadText;
            howManyText = Properties.Resources.howManyText;
            movieText = Properties.Resources.movieText;
            playlistText = Properties.Resources.playlistText;
            processCompleteText = Properties.Resources.processCompleteText;
            saveLocationText = Properties.Resources.saveLocationText;
            seriesText = Properties.Resources.seriesText;
            shutdownPCText = Properties.Resources.shutdownPCText;
            startText = Properties.Resources.startText;

            howManyText = Properties.Resources.howManyText;

            error = Properties.Resources.error;
            selectFolderText = Properties.Resources.selectFolderText;
            downloads = Properties.Resources.downloads;
            noUpdate = Properties.Resources.noUpdate;
            alreadyUpdatedText = Properties.Resources.alreadyupdatedText;
            updatingPleaseWait = Properties.Resources.updatingPleaseWait;
            ytdlpUpdate = Properties.Resources.ytdlpUpdate;
            updateError = Properties.Resources.updateError;
            updateCompletedMessage = Properties.Resources.updateCompletedMessage;
            updateCompleted = Properties.Resources.updateCompletedMessage;
            selectDownloadLocationText = Properties.Resources.selectDownloadLocationText;
            inputCorrectint = Properties.Resources.inputcorrectint;
            movieseriesaudioformat = Properties.Resources.movieseriesaudioformat;
            movieseriesvideoformat = Properties.Resources.movieseriesvideoformat;
            movieserieskeyformat = Properties.Resources.movieserieskeyformat;
            moviename = Properties.Resources.moviename;
            movieyear = Properties.Resources.movieyear;
            seriesname = Properties.Resources.seriesname;
            seriesseason = Properties.Resources.seriesseason;
            seriesstartep = Properties.Resources.seriesstartep;
            moviecompleted = Properties.Resources.moviecompleted;
            episodenumber = Properties.Resources.episodenumber;
            downloadcompleteText = Properties.Resources.downloadcompleteText;
            downloadCompleteMessage = Properties.Resources.downloadCompleteMessage;
            shuttingdownText = Properties.Resources.shuttingdownText;
            enteryoutubelinkText = Properties.Resources.enteryoutubelinkText;
            doformatselectionText = Properties.Resources.doformatselectionText;

            

        }
    }
}