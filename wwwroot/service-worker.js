// ponytail: pure pass-through, no offline caching — this only exists so iOS/Android
// treat the site as installable. Upgrade path: add cache-first for static assets
// (app.tailwind.css, tabler-icons, manga cover images) when real offline support matters.
self.addEventListener('install', () => self.skipWaiting());
self.addEventListener('activate', (event) => event.waitUntil(self.clients.claim()));
self.addEventListener('fetch', (event) => event.respondWith(fetch(event.request)));