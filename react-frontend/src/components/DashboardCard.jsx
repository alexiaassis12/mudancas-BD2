function DashboardCard({ title, value, subtitle }) {
  return (
    <article className="dashboard-card">
      <div className="card-header">
        <span className="card-title">{title}</span>
        <span className="card-value">{value}</span>
      </div>
      <p className="card-subtitle">{subtitle}</p>
    </article>
  )
}

export default DashboardCard;
