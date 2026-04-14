document.addEventListener('DOMContentLoaded', function () {
  var el = document.getElementById('content');
  if (!el) return;

  var mdFile = '../md/' + location.pathname.split('/').pop().replace(/\.html$/, '.md');
  console.log('Trying to fetch:', mdFile);

  fetch(mdFile)
    .then(function (r) { 
      console.log('Fetch response status:', r.status);
      if (!r.ok) throw new Error('HTTP ' + r.status);
      return r.text(); 
    })
    .then(function (md) {
      console.log('Loaded markdown, length:', md.length);
      // Strip top nav block: everything up to and including the first \n\n---\n\n
      var topSep = md.indexOf('\n\n---\n\n');
      if (topSep !== -1) md = md.slice(topSep + 7);

      // Strip bottom nav block: everything from and including the last \n\n---\n\n
      var botSep = md.lastIndexOf('\n\n---\n\n');
      if (botSep !== -1) md = md.slice(0, botSep);

      // Rewrite internal .md links → .html so cross-references work
      md = md.replace(/\(([^)]*?)\.md(#[^)]*?)?\)/g, function (_, path, anchor) {
        var name = (path === 'README') ? 'index' : path;
        return '(html/' + name + '.html' + (anchor || '') + ')';
      });

      el.innerHTML = marked.parse(md);
    })
    .catch(function (error) {
      console.error('Error loading markdown:', error);
      el.innerHTML = '<p style="color: red;">Error loading content: ' + error.message + '</p>';
    });
});
