# Breaking Changes
- The OPDS endpoint has been moved from <code>/opds</code> to <code>/opds/v1</code>.

# Features
- Added support for KOReader Sync.

# Fixes
- KOReader was unable to load the OPDS catalog due to a missing header in the response.

---

## KOReader Sync

As you may be aware, BookHeaven Server has been designed to work in tandem with the dedicated reader app for Android, which will provide the best overall experience.<br/>
However, BookHeaven is all about convenience and adding support for KOReader Sync has been an effort to provide more of that.

So from now on you can use KOReader to read your books and sync your reading progress across devices using KOReader.<br/>
On top of that, your progress will also show up in the web ui, minus the reading time, the sync API doesn't provide that info.<br/>
I know that KOReader does track reading time, 

Sharing progress between the android app and KOReader is not possible though, since the way of handling ebooks and progress is different between the two platforms.

### How to set up

To use KOReader with BookHeaven Server, add your Server as an OPDS catalog:<br/>
```
http://<your-server-ip>:<your-server-port>/opds/v1
```
-- or --
```
https://<your-server-domain>/opds/v1
```

Then, inside progress sync settings, add again your server url as a custom server (just the base url).<br/>
Log in using your BookHeaven profile name as username. The password is not required, but don't leave it blank otherwise it will throw an error.<br/>
And that's it!