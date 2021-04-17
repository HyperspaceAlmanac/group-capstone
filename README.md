## group-capstone

Car rental service where vehicles have electronic locks attached ot them, and there is online platform for customers to search for vehicles and get access codes.

## Database Migrations

Ran into some issues with this. Please delete the ApplicationDBContext snapshot file before creating and running a migration.

## Secrets.cs and values needed

Root of project should have a Secrets.cs file with static Secrets class.

API key strings needed for project to run:

GOOGLE_API_KEY

STRIPES_API_KEY

STRIPES_PUBLIC_KEY

TWILIO_ACCOUNT_SID

TWILIO_AUTH_TOKEN

TWILIO_PHONENUMBER = "+1xxxyyyzzzz" (Phone number that Twilio will send text from)

MY_PHONE_NUMBER = "+1xxxyyyzzzz" (Phone number that Twilio will send text to)


## Presentation was done on commmit #105.

During presentation photos uploaded by customer did not get displayed correctly.

This is due to browser using cached image. AfterPresentation branch made changes to force browser to always grab new images. This has now been merged into main.

Will probably keep on making notes about functionality changes since presentation here.
