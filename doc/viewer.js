document.addEventListener('DOMContentLoaded', function () {
  var el = document.getElementById('content');
  var mdSourceEl = document.getElementById('md-source');
  if (!el || !mdSourceEl) return;

  var md;
  try {
    md = JSON.parse(mdSourceEl.textContent);
  } catch (e) {
    el.innerHTML = '<p style="color: red;">Error parsing content: ' + e.message + '</p>';
    return;
  }

  function slugify(text) {
    return text.toLowerCase()
      .replace(/ä/g, 'ae').replace(/ö/g, 'oe').replace(/ü/g, 'ue').replace(/ß/g, 'ss')
      .replace(/[^\w\s-]/g, '')
      .replace(/\s+/g, '-')
      .replace(/-+/g, '-')
      .replace(/^-|-$/g, '');
  }

  // Strip top nav block: everything up to and including the first \n\n---\n\n
  var topSep = md.indexOf('\n\n---\n\n');
  if (topSep !== -1) md = md.slice(topSep + 7);

  // Strip bottom nav block: everything from and including the last \n\n---\n\n
  var botSep = md.lastIndexOf('\n\n---\n\n');
  if (botSep !== -1) md = md.slice(0, botSep);

  // Rewrite internal .md links → .html so cross-references work
  md = md.replace(/\(([^)]*?)\.md(#[^)]*?)?\)/g, function (_, path, anchor) {
    var name = (path === 'README') ? '../index' : path;
    return '(' + name + '.html' + (anchor || '') + ')';
  });

  el.innerHTML = marked.parse(md);

  // Scale each image to 80% of its intrinsic width
  el.querySelectorAll('img').forEach(function (img) {
    function applySize() {
      if (img.naturalWidth) img.style.width = (img.naturalWidth * 0.8) + 'px';
    }
    if (img.complete) { applySize(); } else { img.addEventListener('load', applySize); }
  });

  // Add IDs to headings that don't already have one
  el.querySelectorAll('h1, h2, h3, h4, h5, h6').forEach(function (h) {
    if (!h.id) {
      h.id = slugify(h.textContent);
    }
  });

  // Scroll to hash after content is rendered
  if (location.hash) {
    var target = document.getElementById(decodeURIComponent(location.hash.slice(1)));
    if (target) target.scrollIntoView({ block: 'start' });
  }
});
