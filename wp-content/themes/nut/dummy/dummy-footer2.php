
<!--FOOTER SIDEBAR-->
    <?php if ( is_active_sidebar( 'foot_sidebar' ) ) { ?>
     <div class="row">
    <div id="footer">
   
     <div class=" large-12">
    
    <div class="widgets"><?php if ( is_active_sidebar('dynamic_sidebar') || !dynamic_sidebar('Footer Widgets') ) : ?><?php endif; ?>
            </div>
   </div>  </div>
   </div>	
     <?php } ?> 
 


	<!--COPYRIGHT TEXT-->
    <div class="row">
    <div id="copyright">
    
    <div class="large-12">
    
            <div class="copytext">
           <?php echo of_get_option('footer_textarea'); ?>
		   <?php _e('Theme by', 'hathor');?> <a target="_blank" href="http://www.imonthemes.com/">Imon Themes</a>
            </div>
        <!--FOOTER MENU-->    
            <div class="social-profiles clearfix">
				
                
           
			</div>
           <a href="#" class="scrollup"> &uarr;</a>   
    </div>
</div>

</div>




