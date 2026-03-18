using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject LoginOrRegister;
    public GameObject Signin;
    public GameObject PassengerOrDriver;
    public GameObject WelcomePage;
    public GameObject HomeSection;
    public GameObject PassengerRegistration;
    public GameObject PRegistrationPage2;
    public GameObject PRegistrationPage3;

    [Header("Footer Sections")]
    public GameObject TripSummary;
    public GameObject Wallet;
    public GameObject SupportSection;
    public GameObject ProfileSection;

    [Header("Profile Sections")]
    public GameObject EditProfileSection;
    public GameObject PasswrodEditSection;

    [Header("Chat Sections")]
    public GameObject ChatSection;
    public GameObject UserToAgentChat;
    public GameObject UserToUserChat;

    [Header("Home Body Sections")]
    public GameObject BookNow;
    public GameObject SheduleLater;
    public GameObject PickUpOrDropOffArea;

    [Header("Booking Flow")]
    public GameObject PopUpModule;
    public GameObject LookingAvailDrivers;
    public GameObject ConfirmDriver;
    public GameObject StartButton;
    public GameObject HomeFooter;

    [Header("Trip Flow")]
    public GameObject EndTrip;

    [Header("Destination Details")]
    public GameObject DetinationsRDetails;

    [Header("Scan Section")]
    public GameObject ScanSection;

    [Header("Payment Approved")]
    public GameObject PaymentApproved;

    [Header("PopUp Schedule Later")]
    public GameObject SheduleLaterPopUp;
    public GameObject SuccessfullScheduled;


    void Start()
    {
        Signin.SetActive(false);
        PassengerOrDriver.SetActive(false);
        WelcomePage.SetActive(false);
        HomeSection.SetActive(false);
        PassengerRegistration.SetActive(false);
        PRegistrationPage2.SetActive(false);
        PRegistrationPage3.SetActive(false);

        TripSummary.SetActive(false);
        Wallet.SetActive(false);
        SupportSection.SetActive(false);
        ProfileSection.SetActive(false);

        EditProfileSection.SetActive(false);
        PasswrodEditSection.SetActive(false);

        ChatSection.SetActive(false);
        UserToAgentChat.SetActive(false);
        UserToUserChat.SetActive(false);

        BookNow.SetActive(false);
        SheduleLater.SetActive(false);
        PickUpOrDropOffArea.SetActive(false);

        PopUpModule.SetActive(false);
        LookingAvailDrivers.SetActive(false);
        ConfirmDriver.SetActive(false);

        EndTrip.SetActive(false);

        DetinationsRDetails.SetActive(false);

        ScanSection.SetActive(false);

        SheduleLaterPopUp.SetActive(false);
        SuccessfullScheduled.SetActive(false);
    }

    // ==============================
    // Helper — Hide All Main Sections
    // ==============================
    void HideAllSections()
    {
        HomeSection.SetActive(false);
        TripSummary.SetActive(false);
        Wallet.SetActive(false);
        SupportSection.SetActive(false);
        ProfileSection.SetActive(false);
    }

    // ==============================
    // Footer Navigation
    // ==============================
    public void FooterGoToHome()
    {
        HideAllSections();
        HomeSection.SetActive(true);
    }

    public void FooterGoToTripSummary()
    {
        HideAllSections();
        TripSummary.SetActive(true);
    }

    public void FooterGoToWallet()
    {
        HideAllSections();
        Wallet.SetActive(true);
    }

    public void FooterGoToSupport()
    {
        HideAllSections();
        SupportSection.SetActive(true);
    }

    public void FooterGoToProfile()
    {
        HideAllSections();
        ProfileSection.SetActive(true);
    }

    // ==============================
    // From LoginOrRegister
    // ==============================
    public void GoToSignIn()
    {
        LoginOrRegister.SetActive(false);
        Signin.SetActive(true);
    }

    public void GoToPassengerOrDriver()
    {
        LoginOrRegister.SetActive(false);
        PassengerOrDriver.SetActive(true);
    }

    // ==============================
    // From PassengerOrDriver
    // ==============================
    public void GoToPassengerRegistration()
    {
        PassengerOrDriver.SetActive(false);
        PassengerRegistration.SetActive(true);
    }

    // ==============================
    // Registration Navigation
    // ==============================
    public void GoToPRegistrationPage2()
    {
        PassengerRegistration.SetActive(false);
        PRegistrationPage2.SetActive(true);
    }

    public void GoToPRegistrationPage3()
    {
        PRegistrationPage2.SetActive(false);
        PRegistrationPage3.SetActive(true);
    }

    // ==============================
    // Registration Back Buttons
    // ==============================
    public void BackToPassengerOrDriver()
    {
        Signin.SetActive(false);
        PassengerRegistration.SetActive(false);
        PassengerOrDriver.SetActive(true);
    }

    public void BackToPassengerRegistration()
    {
        PRegistrationPage2.SetActive(false);
        PassengerRegistration.SetActive(true);
    }

    public void BackToPRegistrationPage2()
    {
        PRegistrationPage3.SetActive(false);
        PRegistrationPage2.SetActive(true);
    }

    // ==============================
    // From SignIn
    // ==============================
    public void GoToWelcomePage()
    {
        Signin.SetActive(false);
        WelcomePage.SetActive(true);
    }

    // ==============================
    // From WelcomePage
    // ==============================
    public void GoToHomeSection()
    {
        WelcomePage.SetActive(false);
        HomeSection.SetActive(true);
    }

    // ==============================
    // Back to LoginOrRegister
    // ==============================
    public void BackToLoginOrRegister()
    {
        Signin.SetActive(false);
        PassengerOrDriver.SetActive(false);
        LoginOrRegister.SetActive(true);
    }

    // ==============================
    // Home Start Button
    // ==============================
    public void OnStartButton()
    {
        BookNow.SetActive(true);
        SheduleLater.SetActive(true);
        PickUpOrDropOffArea.SetActive(true);
    }

    // ==============================
    // Booking Flow
    // ==============================
    public void OnBookNow()
    {
        // Hide home elements
        StartButton.SetActive(false);
        BookNow.SetActive(false);
        SheduleLater.SetActive(false);
        PickUpOrDropOffArea.SetActive(false);
        HomeFooter.SetActive(false);    // ← keep this here

        // Show PopUpModule and LookingAvailDrivers
        PopUpModule.SetActive(true);
        LookingAvailDrivers.SetActive(true);

        StartCoroutine(ShowConfirmDriver());
    }

    IEnumerator ShowConfirmDriver()
    {
        yield return new WaitForSeconds(3f);

        // Hide loading popup
        LookingAvailDrivers.SetActive(false);
        PopUpModule.SetActive(false);

        // DON'T hide HomeSection anymore
        // Just hide the footer
        HomeFooter.SetActive(false);

        // Show ConfirmDriver on top
        ConfirmDriver.SetActive(true);
    }

    // ==============================
    // Profile Navigation
    // ==============================
    public void GoToEditProfile()
    {
        ProfileSection.SetActive(false);
        EditProfileSection.SetActive(true);
    }

    public void GoToPasswordEdit()
    {
        EditProfileSection.SetActive(false);
        PasswrodEditSection.SetActive(true);
    }

    public void BackToEditProfile()
    {
        PasswrodEditSection.SetActive(false);
        EditProfileSection.SetActive(true);
    }

    public void BackToProfile()
    {
        EditProfileSection.SetActive(false);
        PasswrodEditSection.SetActive(false);
        ProfileSection.SetActive(true);
    }

    // ==============================
    // Chat Navigation
    // ==============================
    public void GoToUserToAgentChat()
    {
        SupportSection.SetActive(false);
        ChatSection.SetActive(true);
        UserToAgentChat.SetActive(true);
        UserToUserChat.SetActive(false);
    }

    public void GoToUserToUserChat()
    {
        ChatSection.SetActive(true);
        UserToAgentChat.SetActive(false);
        UserToUserChat.SetActive(true);
    }

    public void BackToSupportSection()
    {
        ChatSection.SetActive(false);
        UserToAgentChat.SetActive(false);
        UserToUserChat.SetActive(false);
        SupportSection.SetActive(true);
    }

    public void GoToTrip()
    {
        ConfirmDriver.SetActive(false);
        EndTrip.SetActive(true);
    }

    public void GoToDestinationDetails()
    {
        EndTrip.SetActive(false);
        DetinationsRDetails.SetActive(true);
    }

    public void GoToScanSection()
    {
        DetinationsRDetails.SetActive(false);
        HomeSection.SetActive(false);
        ScanSection.SetActive(true);
    }

    public void GoToPaymentApproved()
    {
        DetinationsRDetails.SetActive(false);
        PopUpModule.SetActive(true);
        PaymentApproved.SetActive(true);

    }

    public void BackToDestinationDetails()
    {
        ScanSection.SetActive(false);
        HomeSection.SetActive(true);
        DetinationsRDetails.SetActive(true);
    }

    public void BackToHomeSection()
    {
        PaymentApproved.SetActive(false);
        PopUpModule.SetActive(false);
        EndTrip.SetActive(false);
        ConfirmDriver.SetActive(false);

        BookNow.SetActive(false);
        SheduleLater.SetActive(false);
        PickUpOrDropOffArea.SetActive(false);

        HomeSection.SetActive(true);

        HomeFooter.SetActive(true);
        StartButton.SetActive(true);
    }

    public void GoToScheduleLater()
    {
        PopUpModule.SetActive(true);
        SheduleLaterPopUp.SetActive(true);
        SuccessfullScheduled.SetActive(false);
    }

    public void OnConfirmSchedule()
    {
        // Hide schedule popup
        SheduleLaterPopUp.SetActive(false);

        // Show success popup
        SuccessfullScheduled.SetActive(true);

        // After 3 seconds go back to home
        StartCoroutine(BackToHomeAfterSchedule());
    }

    IEnumerator BackToHomeAfterSchedule()
    {
        yield return new WaitForSeconds(3f);

        // Hide everything
        SuccessfullScheduled.SetActive(false);
        PopUpModule.SetActive(false);

        // Reset home to original state
        HomeSection.SetActive(true);
        HomeFooter.SetActive(true);
        StartButton.SetActive(true);
        BookNow.SetActive(false);
        SheduleLater.SetActive(false);
        PickUpOrDropOffArea.SetActive(false);
    }

}
