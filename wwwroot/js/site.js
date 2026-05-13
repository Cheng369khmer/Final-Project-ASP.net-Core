function toggleMenu() {
    const nav = document.getElementById('navLinks');
    nav.classList.toggle('open');
}

// Highlight active nav link
document.addEventListener('DOMContentLoaded', function () {
    const links = document.querySelectorAll('.nav-link');
    const path = window.location.pathname.toLowerCase();
    links.forEach(link => {
        const href = link.getAttribute('href')?.toLowerCase();
        if (href && href !== '/' && path.startsWith(href)) {
            link.classList.add('active');
        } else if (href === '/' && path === '/') {
            link.classList.add('active');
        }
    });
});
