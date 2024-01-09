use KermanshahNew
go

select kafo from VorodiAzadi as v 
where v.kafo not in (select kafo from KafoAzadi) and v.TYPE not in (504,508)
group by kafo


