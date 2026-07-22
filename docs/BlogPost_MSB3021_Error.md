# Fixing MSB3021 Error: "The process cannot access the file because it is being used by another process"

## The Problem

When building a .NET project in Visual Studio, you might encounter this frustrating error:

```
Error (active) MSB3021
Unable to copy file "C:\...\obj\Debug\net10.0\apphost.exe" to "bin\Debug\net10.0\YourApp.exe"
The process cannot access the file 'C:\...\bin\Debug\net10.0\YourApp.exe' because it is being used by another process.
```

This error prevents your project from building successfully.

## Why Does This Happen?

The MSB3021 error occurs when **the executable file is locked by a running process**. The build system tries to copy the newly compiled executable to the output directory, but it can't overwrite the existing file because:

1. **The application is still running** — from a previous debug session or manual execution
2. **The debugger is still attached** — even though you think you stopped debugging
3. **A file watcher or background process has a lock** on the executable
4. **Visual Studio's Hot Reload or auto-restart feature** is keeping the process alive

## How to Resolve It

### Quick Fix (Recommended)

**Step 1: Find and kill the running process**

Open PowerShell and run:
```powershell
Get-Process -Name "YourAppName" -ErrorAction SilentlyContinue | Stop-Process -Force
```

Or in Command Prompt:
```cmd
taskkill /IM YourAppName.exe /F
```

**Step 2: Clean the build**

In Visual Studio:
- Go to **Build → Clean Solution** (or press `Ctrl+Alt+Delete`)
- Or run in PowerShell: `dotnet clean -c Debug`

**Step 3: Rebuild**

Press `Ctrl+Shift+B` to rebuild your solution.

### Alternative Solutions

**Option A: Stop Debugging in Visual Studio**
- Press `Shift+F5` to stop the debugger
- Then rebuild

**Option B: Close Visual Studio Completely**
- **File → Exit**
- Reopen and rebuild

**Option C: Disable Hot Reload**
- Go to **Tools → Options → Debugging**
- Find `.NET/C++` → **Hot Reload**
- Disable the option
- Restart Visual Studio

**Option D: Use Task Manager**
- Press `Ctrl+Shift+Esc`
- Find your application's .exe file
- Right-click → **End Task**
- Then rebuild in Visual Studio

## Prevention Tips

✅ **Best Practices to Avoid This Error:**

1. **Always stop the debugger** before rebuilding (`Shift+F5`)
2. **Close the app window** if running standalone
3. **Use "Clean Solution"** before major rebuilds
4. **Enable Hot Reload wisely** — it can cause issues with file locks
5. **Check Task Manager** for orphaned processes before building

## For .NET Blazor Projects

If you're working with Blazor:

- The dev server (`dotnet watch run`) might keep processes alive
- Stop the dev server before rebuilding: `Ctrl+C` in the terminal
- Consider using `dotnet watch` for development, but stop it before cleaning/rebuilding

## Summary

| Error | Cause | Fix |
|-------|-------|-----|
| MSB3021 | Executable is locked | Kill the process, clean solution, rebuild |
| File in use error | Debugger still attached | Press `Shift+F5` to stop debugging |
| Persistent lock | Hot Reload enabled | Disable in Tools → Options → Debugging |

If the error persists after trying these steps, restart Visual Studio entirely.

---

**Have you encountered this issue? Share your solution in the comments!**
