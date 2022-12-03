using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TitleScreenMenus : MonoBehaviour
{
    public GameObject infopage;
    public GameObject rulespage;
    public GameObject settingspage;
    public GameObject TitleScreen;
    public GameObject box;
    public GameObject moreSettings;
    public GameObject controlsPage;
    public Sprite on;
    public Sprite off;
    public RuleSetter[] rules;
    public static bool[] ruleset = null;
    public bool rulesHaveBeenSet = false;
    public Text boxtitle;
    public Text boxdescr;
    public Image boxim;
    public Animator anim;
    public AudioMixer audiomixer;
    public Resolution[] resolutions;
    public Dropdown graphicsDropDown;
    public Dropdown resolutionDropdown;
    public Scrollbar volumeSlider;
    public Toggle fullScreen;
    public AudioManagerScript AudioManagerScript;

    void Start()
    {
        AudioManagerScript.Play("Track2");

        int i = 0;

        foreach (RuleSetter r in rules)
        {
            if (PlayerPrefs.GetFloat("rule" + i.ToString()) == 0)
            {
                r.usingInGame = true;
                r.image.sprite = on;
            }
            else
            {
                r.usingInGame = false;
                r.image.sprite = off;
            }
            i++;
        }

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentresolutionindex = 0;

        if (PlayerPrefs.GetInt("graphics") != 0 || PlayerPrefs.GetInt("resolution") != 0 || PlayerPrefs.GetFloat("volume") != 0 || PlayerPrefs.GetInt("fullscreen") != 0)
        {
            graphicsDropDown.value = PlayerPrefs.GetInt("graphics");
            graphicsDropDown.RefreshShownValue();
            resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
            resolutionDropdown.RefreshShownValue();
            volumeSlider.value = PlayerPrefs.GetFloat("volume") / -80;

            if (PlayerPrefs.GetInt("fullscreen") == 0)
            {
                fullScreen.isOn = true;
                Screen.fullScreen = true;
            }
            else
            {
                fullScreen.isOn = false;
                Screen.fullScreen = false;
            }
        }

        for (int x = 0; x < resolutions.Length; x++)
        {
            string option = resolutions[x].width + " x " + resolutions[x].height;
            options.Add(option);

            if (resolutions[x].width == Screen.currentResolution.width && resolutions[x].height == Screen.currentResolution.height)
            {
                currentresolutionindex = x;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentresolutionindex;
        resolutionDropdown.RefreshShownValue();
        graphicsDropDown.value = 3;
        graphicsDropDown.RefreshShownValue();
    }

    public void moreSettingsChange()
    {
        infopage.SetActive(false);
        rulespage.SetActive(false);
        settingspage.SetActive(false);
        TitleScreen.SetActive(false);
        moreSettings.SetActive(true);
        controlsPage.SetActive(false);
    }

    public void StartGame()
    {
        int i = 0;

        if (!rulesHaveBeenSet)
        {
            ruleset = new bool[5] { true, true, true, true, true };
        }

        foreach(RuleSetter r in rules)
        {
            ruleset[i] = r.usingInGame;

            i++;
        }
    }

    public void infoPageMethod()
    {
        moreSettings.SetActive(false);
        infopage.SetActive(true);
        rulespage.SetActive(false);
        settingspage.SetActive(false);
        TitleScreen.SetActive(false);
        controlsPage.SetActive(false);
    }

    public void rulesPageMethod()
    {
        moreSettings.SetActive(false);
        infopage.SetActive(false);
        rulespage.SetActive(true);
        settingspage.SetActive(false);
        TitleScreen.SetActive(false);
        controlsPage.SetActive(false);
    }

    public void settingsPageMethod()
    {
        moreSettings.SetActive(false);
        infopage.SetActive(false);
        rulespage.SetActive(false);
        settingspage.SetActive(true);
        TitleScreen.SetActive(false);
        rulesHaveBeenSet = true;
        controlsPage.SetActive(false);
    }

    public void controlsPageMethod()
    {
        moreSettings.SetActive(false);
        infopage.SetActive(false);
        rulespage.SetActive(false);
        settingspage.SetActive(false);
        TitleScreen.SetActive(false);
        controlsPage.SetActive(true);
    }

    public void closeAllMethod()
    {
        moreSettings.SetActive(false);
        infopage.SetActive(false);
        rulespage.SetActive(false);
        settingspage.SetActive(false);
        TitleScreen.SetActive(true);
        controlsPage.SetActive(false);

        helpclosefunct();
    
        int i = 0;

        ruleset = new bool[5] { true, true, true, true, true };

        foreach (RuleSetter r in rules)
        {
            ruleset[i] = r.usingInGame;

            if (r.usingInGame)
            {
                PlayerPrefs.SetFloat("rule" + i.ToString(), 0);
            }
            else
            {
                PlayerPrefs.SetFloat("rule" + i.ToString(), 1);
            }

            i++;
        }

        i = 0;

        foreach (RuleSetter r in rules)
        {
            if (ruleset[i])
            {
                r.usingInGame = true;
                r.image.sprite = on;
            }
            else
            {
                r.usingInGame = false;
                r.image.sprite = off;
            }
    
            i++;
        }
    }
    
    public void changeRule(string rulename)
    {
        RuleSetter match = null;
        foreach (RuleSetter rule in rules)
        {
            bool isMatch = false;
            if (rule.rulename == rulename)
            {
                match = rule;
                isMatch = true;
            }
            if (isMatch) break;
        }
    
        if (match != null)
        {
            if (match.usingInGame)
            {
                match.image.sprite = off;
            }
            else
            {
                match.image.sprite = on;
            }
            match.usingInGame = !match.usingInGame;
        }
    }

    public void helpFunct(string helpname)
    {
        RuleSetter match = null;
        foreach (RuleSetter rule in rules)
        {
            bool isMatch = false;
            if (rule.helpicon.name == helpname)
            {
                match = rule;
                isMatch = true;
            }
            if (isMatch) break;
        }

        if (match != null)
        {
            boxtitle.text = match.titlename;
            boxdescr.text = match.Description;
            boxim.sprite = match.DescriptionPicture;

            box.SetActive(true);
        }
    }

    public void helpclosefunct()
    {
        box.SetActive(false);
    }

    public void setVolume(float volume)
    {
        audiomixer.SetFloat("volume", -80 * volume);
        PlayerPrefs.SetFloat("volume", -80 * volume);
    }

    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("quality", index);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen)
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
    }
    public void setResolution(int index)
    {
        Resolution resolutionSet = resolutions[index];
        Screen.SetResolution(resolutionSet.width, resolutionSet.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", index);
    }
}