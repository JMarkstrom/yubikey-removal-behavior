 Message Compiler definition for YubiKey Removal Behavior
 -----------------------------------------------------------------------------
 Application lifecycle
 -----------------------------------------------------------------------------
//
//  Values are 32 bit values laid out as follows:
//
//   3 3 2 2 2 2 2 2 2 2 2 2 1 1 1 1 1 1 1 1 1 1
//   1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0 9 8 7 6 5 4 3 2 1 0
//  +---+-+-+-----------------------+-------------------------------+
//  |Sev|C|R|     Facility          |               Code            |
//  +---+-+-+-----------------------+-------------------------------+
//
//  where
//
//      Sev - is the severity code
//
//          00 - Success
//          01 - Informational
//          10 - Warning
//          11 - Error
//
//      C - is the Customer code flag
//
//      R - is a reserved bit
//
//      Facility - is the facility code
//
//      Code - is the facility's status code
//
//
// Define the facility codes
//
#define FACILITY_SYSTEM                  0x0
#define FACILITY_YUBIKEY                 0x1


//
// Define the severity codes
//
#define STATUS_SEVERITY_SUCCESS          0x0
#define STATUS_SEVERITY_INFORMATIONAL    0x1
#define STATUS_SEVERITY_WARNING          0x2
#define STATUS_SEVERITY_ERROR            0x3


//
// MessageId: MSG_APP_STARTED
//
// MessageText:
//
// Application started.
//
#define MSG_APP_STARTED                  ((DWORD)0x40010001L)

//
// MessageId: MSG_APP_STOPPED
//
// MessageText:
//
// Application stopped.
//
#define MSG_APP_STOPPED                  ((DWORD)0x40010002L)

 -----------------------------------------------------------------------------
 YubiKey removal events
 -----------------------------------------------------------------------------
//
// MessageId: MSG_REMOVED_LOCK
//
// MessageText:
//
// YubiKey removed (locking the workstation).
//
#define MSG_REMOVED_LOCK                 ((DWORD)0x40010100L)

//
// MessageId: MSG_REMOVED_LOGOUT
//
// MessageText:
//
// YubiKey removed (logging off the current user).
//
#define MSG_REMOVED_LOGOUT               ((DWORD)0x40010101L)

//
// MessageId: MSG_REMOVED_NOACTION
//
// MessageText:
//
// YubiKey removed with no action taken (removalOption: %1).
//
#define MSG_REMOVED_NOACTION             ((DWORD)0x80010102L)

