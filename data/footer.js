document.addEventListener("DOMContentLoaded", function () {
  var prefix = "";
  var scripts = document.getElementsByTagName('script');
  for (var i = 0; i < scripts.length; i++) {
    var src = scripts[i].getAttribute('src');
    if (src && src.endsWith('data/footer.js')) {
      prefix = src.replace('data/footer.js', '');
      break;
    }
  }

  var footerHTML = `
  <footer>
    <p>
      <a href="${prefix}impressum.html">Impressum</a> |
      <a href="${prefix}datenschutz.html">Datenschutzerklärung</a>
    </p>
  </footer>`;
  document.body.insertAdjacentHTML('beforeend', footerHTML);
});
