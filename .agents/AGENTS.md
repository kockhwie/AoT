# AOT Project — Agent Rules

These rules are **always enforced** when working on this project. They capture hard-won lessons from previous sessions to prevent regression.

---

## CSS Architecture Rules

### Blazor CSS Isolation
- **NEVER use :root CSS custom properties inside *.razor.css files.** Blazor's CSS isolation scoping mechanism breaks :root variable resolution — the variables compile fine but fail to cascade into scoped component elements, causing all text to collapse to the browser default size.
- **ALWAYS declare font sizes, colors, and spacing as explicit values directly inside class rules** in *.razor.css files (e.g. ont-size: 1.25rem not ont-size: var(--my-token)).
- CSS custom properties (variables) are only safe in **global** files like wwwroot/app.css or wwwroot/css/site.css.

---

## Typography Rules

### Clear 3-Tier Visual Hierarchy — mandatory on every card and section
Every card or data section must have a visually distinct hierarchy:

| Tier | Role | Example Size | Color |
|------|------|-------------|-------|
| **1 — Title** | Character name, gear name | `1.25rem` bold | `#ffffff` bright white |
| **2 — Subtitle** | Military role, tech classification | `0.875rem` medium weight | `#dfbb6b` warm brass gold |
| **3 — Body / Details** | Description prose, stats, specs | `0.875rem` regular | `#d1d5db` neutral light grey |

- The three tiers **must be visually distinguishable** at a glance. If all text looks the same size or colour, the layout has failed.
- On mobile, body text should be at least `0.9rem` for CJK character readability.

### Font Family
- **NEVER apply `font-family: monospace`** to any UI text, badge, label, or tag. Monospace creates a visually jarring mismatch against the rest of the page.
- Use the system sans-serif stack everywhere. Monospace is only acceptable inside actual code blocks.

---

## Card Design Rules

### Border Treatment
- **NEVER use `border-left` alone** on cards that have `border-radius`. A thick left edge clips visually against the curved corner and looks broken.
- **ALWAYS use uniform, all-around `border` outlines** on rounded cards. To differentiate card themes, change the `border-color` (not `border-left-color`).
- Division/category theming on cards must be expressed via the **uniform border colour** and optionally via **badge colour** — never via asymmetric edge accents.

### Card Hover Effects
- Keep hover transforms subtle: `translateY(-2px)` max. Bigger lifts feel cheap.
- Always pair hover with a matching `border-color` and `box-shadow` brightening.

---

## Design Language

### What "premium" looks like for this project
- Dark obsidian card backgrounds with subtle gradient: `linear-gradient(135deg, rgba(20,22,26,0.85), rgba(12,13,16,0.95))`
- Subtle gold wireframe borders at rest (`rgba(255,215,0,0.15)`), brighter on hover (`rgba(255,215,0,0.35)`)
- Text hierarchy: white → brass gold → neutral grey
- No loud neon text on dark backgrounds — reserve neon only for badges and small accents
- Section labels: uppercase, wide letter-spacing (`0.18em`), clean sans-serif

### What to avoid
- Mixing bright saturated coloured text (blue, purple, pink) into card body text
- Making every element the same font size — hierarchy is the difference between an app and a spreadsheet
- Using `font-family: monospace` in UI elements
- Using `border-left` on rounded cards
- Using `:root` variables inside `*.razor.css` files
