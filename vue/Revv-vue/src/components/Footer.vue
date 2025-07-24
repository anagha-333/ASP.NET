<template>
  <footer class="custom-footer">
    <div class="container">
      <div class="row">
        <div class="col-md-4 mb-4 mb-md-0 d-flex flex-column align-items-md-start align-items-center">
          <div class="footer-logo">
            <img src="@/assets/logo1.svg" alt="REVV Logo" />
          </div>
          <div class="app-badges">
            <a href="https://play.google.com/store/apps/details?id=com.revv.vin" target="_blank">
              <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Google_Play_Store_badge_EN.svg/2560px-Google_Play_Store_badge_EN.svg.png" alt="Get it on Google Play" />
            </a>
            <a href="https://apps.apple.com/us/app/revv-ai/id6740824555" target="_blank">
              <img src="https://developer.apple.com/assets/elements/badges/download-on-the-app-store.svg" alt="Download on the App Store" />
            </a>
          </div>
          <div class="footer-copyright">
            &copy; {{ year }} REVV. All rights reserved.
          </div>
        </div>

        <div class="col-md-5 d-none d-md-block"></div>

        <div class="col-md-3 d-flex justify-content-md-end justify-content-center">
          <div class="footer-links">
            <ul>
              <li><a href="https://app-revvapp-qa-eastus.azurewebsites.net/about-us">About us</a></li>
              <li><a href="https://app-revvapp-qa-eastus.azurewebsites.net/privacy-policy">Privacy policy</a></li>
              <li><a href="https://app-revvapp-qa-eastus.azurewebsites.net/terms-of-use">Terms of use</a></li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script setup lang="ts">
const year = new Date().getFullYear()
</script>
