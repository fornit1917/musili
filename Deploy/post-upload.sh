cd /srv/musili

echo "Install..."
mkdir app-next
tar -xzf dist.tar.gz -C app-next/
sudo chown -R musili app-next
sudo chgrp -R musili app-next

sudo rm -rf app-prev

sudo service musili stop

sudo rm app
mv app-current app-prev
mv app-next app-current
ln -s app-current app

sudo service musili start
sudo service musili status

echo "Done"
