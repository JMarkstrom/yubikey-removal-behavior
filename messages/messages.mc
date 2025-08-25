; Message Compiler definition for YubiKey Removal Behavior

MessageIdTypedef=DWORD

SeverityNames=(
    Success=0x0:STATUS_SEVERITY_SUCCESS
    Informational=0x1:STATUS_SEVERITY_INFORMATIONAL
    Warning=0x2:STATUS_SEVERITY_WARNING
    Error=0x3:STATUS_SEVERITY_ERROR
)

FacilityNames=(
    System=0x0:FACILITY_SYSTEM
    YubiKey=0x1:FACILITY_YUBIKEY
)

LanguageNames=(
    English=0x409:MSG00409
)

; -----------------------------------------------------------------------------
; Application lifecycle
; -----------------------------------------------------------------------------
MessageId=0x0001
Severity=Informational
Facility=YubiKey
SymbolicName=MSG_APP_STARTED
Language=English
Application started.
.

MessageId=0x0002
Severity=Informational
Facility=YubiKey
SymbolicName=MSG_APP_STOPPED
Language=English
Application stopped.
.

; -----------------------------------------------------------------------------
; YubiKey removal events
; -----------------------------------------------------------------------------
MessageId=0x0100
Severity=Informational
Facility=YubiKey
SymbolicName=MSG_REMOVED_LOCK
Language=English
YubiKey removed (locking the workstation).
.

MessageId=0x0101
Severity=Informational
Facility=YubiKey
SymbolicName=MSG_REMOVED_LOGOUT
Language=English
YubiKey removed (logging off the current user).
.

MessageId=0x0102
Severity=Warning
Facility=YubiKey
SymbolicName=MSG_REMOVED_NOACTION
Language=English
YubiKey removed with no action taken (removalOption: %1).
.
