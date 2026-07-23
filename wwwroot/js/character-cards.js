(() => {
    const selector = '[data-holo-card]';

    const update = (card, event) => {
        const rect = card.getBoundingClientRect();
        const x = Math.max(0, Math.min(1, (event.clientX - rect.left) / rect.width));
        const y = Math.max(0, Math.min(1, (event.clientY - rect.top) / rect.height));
        card.style.setProperty('--pointer-x', `${(x * 100).toFixed(2)}%`);
        card.style.setProperty('--pointer-y', `${(y * 100).toFixed(2)}%`);
        card.style.setProperty('--pointer-angle', `${(x * 180 + y * 40).toFixed(1)}deg`);
        card.classList.add('is-holo-active');
    };

    const reset = card => {
        card.classList.remove('is-holo-active');
        card.style.removeProperty('--pointer-x');
        card.style.removeProperty('--pointer-y');
        card.style.removeProperty('--pointer-angle');
    };

    const bind = card => {
        if (card.dataset.holoBound) return;
        card.dataset.holoBound = 'true';
        card.addEventListener('pointerenter', event => update(card, event));
        card.addEventListener('pointermove', event => update(card, event));
        card.addEventListener('pointerdown', event => {
            card.setPointerCapture?.(event.pointerId);
            update(card, event);
        });
        card.addEventListener('pointerleave', () => reset(card));
        card.addEventListener('pointerup', () => reset(card));
        card.addEventListener('pointercancel', () => reset(card));
    };

    const bindAll = () => document.querySelectorAll(selector).forEach(bind);
    bindAll();
    new MutationObserver(bindAll).observe(document.body, { childList: true, subtree: true });
})();
